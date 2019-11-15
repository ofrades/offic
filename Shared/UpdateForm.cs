namespace Shared {
	/// <summary>
	/// UpdateForm
	/// </summary>
	public class UpdateForm {

		/// <summary>
		/// Update Form
		/// </summary>
		/// <param name="_contentUpdated"></param>
		public UpdateForm(string _contentUpdated){
			Content = _contentUpdated;
		}
		
		/// <summary>
		/// UpdateForm
		/// </summary>
		public UpdateForm(){}

		/// <summary>
		/// UpdatedContent
		/// </summary>
		/// <value></value>
		public string Content;
	}
	
}