using System;
using System.Windows.Input;

namespace TruckCall.BusinessLayer.ViewModels {
    #region [ Delegates ]

    /// <summary>
    /// The CommandEventHandler delegate.
    /// </summary>
    public delegate void CommandEventHandler(object sender, CommandEventArgs args);
    /// <summary>
    /// The CancelCommandEvent delegate.
    /// </summary>
    public delegate void CancelCommandEventHandler(object sender, CancelCommandEventArgs args);

    #endregion [ Delegates ]

    #region [ EventArgs classes ]

    /// <summary>
    /// CommandEventArgs - simply holds the command parameter.
    /// </summary>
    public class CommandEventArgs : EventArgs {
        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public object Parameter { get; set; }
    }
    /// <summary>
    /// CancelCommandEventArgs - just like above but allows the event to 
    /// be cancelled.
    /// </summary>
    public class CancelCommandEventArgs : CommandEventArgs {
        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="CancelCommandEventArgs"/> command should be cancelled.
        /// </summary>
        /// <value><c>true</c> if cancel;
        /// otherwise, <c>false</c>.</value>
        public bool Cancel { get; set; }
    }

    #endregion [ EventArgs classes ]

    /// <summary>
    /// Represents command for MVVM
    /// </summary>
    public class Command : ICommand {
        #region [ Private Fields ]

        /// <summary>
        /// Can command execute
        /// </summary>
        private bool _canExecute = false;

        #endregion [ Private Fields ]

        #region [ Protected Fields ]

        /// <summary>
        /// Command action
        /// </summary>
        protected Action _action = null;
        /// <summary>
        /// Parametrized command action
        /// </summary>
        protected Action<object> _parameterizedAction = null;

        #endregion [ Protected Fields ]

        #region [ Properties ]

        /// <summary>
        /// Gets or sets can command execute
        /// </summary>
        public bool CanExecute {
            get { return _canExecute; }
            set {
                if (_canExecute != value) {
                    _canExecute = value;
                    EventHandler canExecuteChanged = CanExecuteChanged;
                    if (canExecuteChanged != null)
                        canExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion [ Properties ]

        #region [ Constructors / Destructors ]

        /// <summary>
        /// Constructor for simple action
        /// </summary>
        /// <param name="action">Command action</param>
        /// <param name="canExecute">Can command execute</param>
        public Command(Action action, bool canExecute = true) {
            this._action = action;
            this._canExecute = canExecute;
        }
        /// <summary>
        /// Contructor for parametrixed action
        /// </summary>
        /// <param name="parameterizedAction">Parameterized action</param>
        /// <param name="canExecute">Can command execute</param>
        public Command(Action<object> parameterizedAction, bool canExecute = true) {
            this._parameterizedAction = parameterizedAction;
            this._canExecute = canExecute;
        }

        #endregion [ Constructors / Destructors ]

        #region [ Private Methods ]

        /// <summary>
        /// Gets can command be executed
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns></returns>
        bool ICommand.CanExecute(object parameter) {
            return _canExecute;
        }
        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter"></param>
        void ICommand.Execute(object parameter) {
            this.DoExecute(parameter);
        }

        #endregion [ Private Methods ]

        #region [ Protected Methods ]

        /// <summary>
        /// Invokes action
        /// </summary>
        /// <param name="param">Action parameters</param>
        protected void InvokeAction(object param) {
            Action theAction = _action;
            Action<object> theParameterizedAction = _parameterizedAction;
            if (theAction != null)
                theAction();
            else if (theParameterizedAction != null)
                theParameterizedAction(param);
        }
        /// <summary>
        /// Invoke execution
        /// </summary>
        /// <param name="args">Command arguments</param>
        protected void InvokeExecuted(CommandEventArgs args) {
            CommandEventHandler executed = Executed;

            // Raise events
            if (executed != null)
                executed(this, args);
        }
        /// <summary>
        /// Invoke executing
        /// </summary>
        /// <param name="args">Command arguments</param>
        protected void InvokeExecuting(CancelCommandEventArgs args) {
            CancelCommandEventHandler executing = Executing;

            // Call the executed event.
            if (executing != null)
                executing(this, args);
        }

        #endregion [ Protected Methods ]

        #region [ Events ]

        /// <summary>
        /// Occurs when can execute options has been changed
        /// </summary>
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Occurs when command has been execiting
        /// </summary>
        public event CancelCommandEventHandler Executing;
        /// <summary>
        /// Occurs when command has been executed
        /// </summary>
        public event CommandEventHandler Executed;

        #endregion [ Events ]

        #region [ Public Methods ]

        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="param">Command parameters</param>
        public virtual void DoExecute(object param) {
            // Start command executing
            CancelCommandEventArgs args = new CancelCommandEventArgs() {
                Parameter = param,
                Cancel = false
            };
            InvokeExecuting(args);

            // Cancel command if necessary
            if (args.Cancel)
                return;

            // Call action
            InvokeAction(param);

            // Call the executed function.
            InvokeExecuted(new CommandEventArgs() { Parameter = param });
        }

        #endregion [ Public Methods ]
    }
}
