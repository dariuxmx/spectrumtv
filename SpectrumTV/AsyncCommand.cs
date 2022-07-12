using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpectrumTV
{
    public class AsyncCommand : ICommand
    {
        private interface IExceptionHandler
        {
            void Handle(Exception ex, object parameter);
        }

        private class ExceptionHandler<T> : IExceptionHandler where T : Exception
        {
            private Action<T, object> _handleException;

            public ExceptionHandler(Action<T, object> handleException)
            {
                _handleException = handleException;
            }

            public ExceptionHandler(Action<T> handleException) : this((ex, obj) => handleException(ex)) { }

            public void Handle(Exception ex, object parameter)
            {
                _handleException(ex as T, parameter);
            }
        }

        private readonly Func<object, CancellationToken, Task> _command;
        private readonly TimeSpan _timeout;
        private readonly Func<object, bool> _canExecute;
        private readonly Dictionary<Type, IExceptionHandler> _exceptionHandlers = new Dictionary<Type, IExceptionHandler>();

        private CancellationTokenSource _commandCts;

        // DELETE
        private Action<CancellationToken> perform_Back_To_ScanCoil;

        public bool IsExecuting { get; private set; }

        public event EventHandler CanExecuteChanged;

        public AsyncCommand(Func<object, CancellationToken, Task> command, TimeSpan timeout = default, Func<object, bool> canExecute = null)
        {
            _command = command;
            _timeout = timeout;
            _canExecute = canExecute;
        }

        // Convenience initializer for commands that do not need a parameter
        public AsyncCommand(Func<CancellationToken, Task> command, TimeSpan timeout = default, Func<bool> canExecute = null) :
            this((obj, cancellationToken) => command(cancellationToken), timeout, obj => canExecute == null || canExecute())
        { }

        // DELETE
        public AsyncCommand(Action<CancellationToken> perform_Back_To_ScanCoil)
        {
            this.perform_Back_To_ScanCoil = perform_Back_To_ScanCoil;
        }

        public bool CanExecute(object parameter)
        {
            return !IsExecuting && (_canExecute == null || _canExecute(parameter));
        }

        public void ChangeCanExecute()
        {
            try
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AsyncCommand] ChangeCanExecute exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Executes the command.
        /// Use this method when you need to run the command from a synchronous method without a timeout or existing token.
        /// This method is used most often in UI-bound commands.
        /// </summary>
        /// <param name="parameter"></param>
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// Use this method when you need to run the command from a synchronous method with a specified timeout or existing token.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="cancellationToken"></param>
        public async void Execute(object parameter, CancellationToken cancellationToken)
        {
            await ExecuteAsync(parameter, cancellationToken);
        }

        /// <summary>
        /// Executes the command asynchronously.
        /// Use this method when you do NOT need to specify a timeout and want to cancel the command using <see cref="AsyncCommand.Cancel()"/>
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter))
            {
                // Create a CTS so this command may be cancelled by the user through Cancel()
                _commandCts = new CancellationTokenSource();

                if (_timeout != default)
                {
                    _commandCts.CancelAfter(_timeout);
                }

                await TryExecuteAsync(parameter, _commandCts.Token);
            }
        }

        /// <summary>
        /// Executes the command asynchronously using an existing cancellation token.
        /// Use this method when you want to specify a timeout or want to cancel the command using an existing token.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(object parameter, CancellationToken cancellationToken)
        {
            if (CanExecute(parameter))
            {
                // Create a CTS so the command may be cancelled by the user through Cancel()
                _commandCts = new CancellationTokenSource();

                if (_timeout != default)
                {
                    _commandCts.CancelAfter(_timeout);
                }

                // If the token is cancelled, cancel the inner task
                cancellationToken.Register(() => _commandCts.Cancel());

                await TryExecuteAsync(parameter, _commandCts.Token);
            }
        }

        private async Task TryExecuteAsync(object parameter, CancellationToken cancellationToken)
        {
            try
            {
                IsExecuting = true;

                ChangeCanExecute();

                await _command(parameter, cancellationToken);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AsyncCommand] Exception thrown: {ex.Message}");

                if (_exceptionHandlers.TryGetValue(ex.GetType(), out IExceptionHandler exceptionHandler))
                {
                    exceptionHandler.Handle(ex, parameter);
                }
                else if (_exceptionHandlers.TryGetValue(typeof(Exception), out IExceptionHandler genericExceptionHandler))
                {
                    genericExceptionHandler.Handle(ex, parameter);
                }

                // Cancel any subtasks
                _commandCts?.Cancel();
            }
            finally
            {
                IsExecuting = false;

                ChangeCanExecute();
            }
        }

        public void Cancel()
        {
            try
            {
                _commandCts?.Cancel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AsyncCommand] Cancel exception: {ex.Message}");
            }
            finally
            {
                _commandCts = null;
            }
        }

        public void Catch<T>(Action<T, object> handleException) where T : Exception
        {
            _exceptionHandlers[typeof(T)] = new ExceptionHandler<T>(handleException);
        }

        public void Catch<T>(Action<T> handleException) where T : Exception
        {
            _exceptionHandlers[typeof(T)] = new ExceptionHandler<T>(handleException);
        }
    }
}
