namespace Shared {
	/// <summary>
	/// UpdateForm
	/// </summary>
	public class CreateContent {

		/// <summary>
		/// Create Content
		/// </summary>
		/// <param name="content"></param>
		/// <param name="title"></param>
		public CreateContent(string content, string title){
			Content = content;
			Title = title;
		}
		
		/// <summary>
		/// UpdateForm
		/// </summary>
		public CreateContent(){}

		/// <summary>
		/// New File Content
		/// </summary>
		/// <value></value>
		public string Content;

		/// <summary>
		/// Title of New File
		/// </summary>
		/// <value></value>
		public string Title;
	}
	
}