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

		public ServiceResult(Exception exception) {
			Exception = exception;
		}

		public ServiceResult() {
		}

		public static T ExceptionFactory<T>(Exception exception) where T : ServiceResult, new() {
			return new T() { Exception = exception };
		}
	}

	public class ServiceResult<T> : ServiceResult {
		public T Result { get; private set; }

		public ServiceResult(T result) {
			Result = result;
		}

		public ServiceResult() {
		}
	}
}
