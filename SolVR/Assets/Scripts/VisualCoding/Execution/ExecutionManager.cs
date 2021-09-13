using System;
using System.Collections.Generic;
using Patterns;
using Robots;
using Robots.Enums;
using UnityEngine;
using VisualCoding.Blocks;
using VisualCoding.Blocks.ActionBlocks;
using VisualCoding.Blocks.LogicBlocks;

namespace VisualCoding.Execution
{
    public class ExecutionManager : Singleton<ExecutionManager>
    {
        [SerializeField] private Robot robot;
        private readonly List<ThreadHandler> _threadHandlers = new List<ThreadHandler>();

        private void Start()
        {
            var startBlocks = FindObjectsOfType<StartBlock>();

            foreach (var startBlock in startBlocks)
            {
                var handler = new ThreadHandler(this, startBlock);
                handler.ThreadId = robot.CreateThread(handler.StateChangeHandler);
                handler.StartExecuting();
                _threadHandlers.Add(handler);
            }
        }

        private void ExitAllThreads()
        {
            foreach (var handler in _threadHandlers)
                handler.StopExecution();

            _threadHandlers.Clear();
        }

        private class ThreadHandler
        {
            public int ThreadId;
            private readonly ExecutionManager _manager;
            private Block _currentBlock;
            private bool _exit;

            public ThreadHandler(ExecutionManager manager, Block currentBlock)
            {
                _manager = manager;
                _currentBlock = currentBlock;
            }

            private void HandleNextBlock()
            {
                while (_currentBlock != null || _exit)
                {
                    _currentBlock = _currentBlock.NextBlock();
                    if (!Step())
                        return;
                }

                _manager.robot.DeleteThread(ThreadId);
            }

            private bool Step()
            {
                if (_currentBlock.GetType() == typeof(EndBlock))
                {
                    _manager.ExitAllThreads();
                    return false;
                }

                if (_currentBlock is IGetCommand actionBlock)
                {
                    var command = actionBlock.GetCommand();
                    _manager.robot.ExecuteCommandOnThread(ThreadId, command);
                    return false;
                }

                return true;
            }

            public void StateChangeHandler(ThreadState changedTo)
            {
                Debug.Log($"Thread {ThreadId} is {Enum.GetName(typeof(ThreadState), changedTo)}!");
                switch (changedTo)
                {
                    case ThreadState.Idle:
                        HandleNextBlock();
                        break;
                    case ThreadState.Executing:
                        break;
                    case ThreadState.Blocked:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(changedTo), changedTo, null);
                }
            }

            internal void StartExecuting()
            {
                HandleNextBlock();
            }

            public void StopExecution()
            {
                _exit = true;
            }
        }
    }
}