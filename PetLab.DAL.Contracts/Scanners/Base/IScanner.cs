namespace PetLab.DAL.Contracts.Scanners.Base {

	public delegate void ScannerReceived(object entry);

	/// <summary>
	/// интерфейс процесса сканера
	/// </summary>
	public interface IScanner {
		/// <summary>
		/// событие получения нового файла
		/// </summary>
		event ScannerReceived Received;
		/// <summary>
		/// процесс запущен или нет
		/// </summary>
		bool IsRunning { get; }
		/// <summary>
		/// запустить процесс
		/// </summary>
		void Start();
		/// <summary>
		/// остановить процесс
		/// </summary>
		void Stop();
	}
}
