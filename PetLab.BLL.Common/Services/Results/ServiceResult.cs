using System;

namespace PetLab.BLL.Common.Services.Results {
	public class ServiceResult {
		private Exception _exception;
		public bool Successed { get; private set; } = true;

		public Exception Exception {
			get { return _exception; }
			private set {
				_exception = value;
				Successed = false;
			}
		}

		public ServiceResult() {
		}

		public ServiceResult(Exception exception) {
			Exception = exception;
		}

		public Exception GetBaseException() {
			return Exception.GetBaseException();
		}

		/// <summary>
		/// throw exception if occurred error
		/// </summary>
		public virtual void CheckResult() {
			if (Successed == false) {
				throw GetBaseException();
			}
		}

		#region factory

		public static T ExceptionFactory<T>(Exception exception) where T : ServiceResult, new() {
			return new T() { Exception = exception };
		}

		#endregion factory

	}

	public class ServiceResult<T> : ServiceResult {
		private T _result;

		public ServiceResult(T result) {
			_result = result;
		}

		public ServiceResult() {
		}

		/// <summary>
		/// throw exception if occurred error
		/// </summary>
		public T GetResult() {
			if (Successed == true) {
				return _result;
			} else {
				throw GetBaseException();
			}
		}
	}
}
