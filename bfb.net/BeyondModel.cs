using System;
using System.Collections.Generic;

namespace bfbnet
{
	public class BeyondCharacterModel
	{
		public String pageName { get; set; }
		public String characterRightHandSideImage { get; set; }
		public String characterDescription { get; set; }
		public String characterStats { get; set; }
		public List<String> characterScreenshotsConceptArt { get; set; }
	}
	public class BeyondRootModel
	{
		public String pageName { get; set; }
		public String pageType { get; set; }
		public String slideRightImage { get; set; }
		public String slideContent { get; set; }
		public List<BeyondCharacterModel> characters { get; set; }
		public List<String> screenshots { get; set; }
		public List<String> conceptart { get; set; }
		public String copyrightBox { get; set; }
		public String copyrightInformation { get; set; }
	}
}

