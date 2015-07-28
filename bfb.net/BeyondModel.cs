using System;
using System.Collections.Generic;

namespace bfb.net
{
	public class BeyondCharacterModel
	{
		public string pageName { get; set; }
		public string characterRightHandSideImage { get; set; }
		public string characterDescription { get; set; }
		public string characterStats { get; set; }
		public IList<string> characterScreenshotsConceptArt { get; set; }
	}
	public class BeyondRootModel
	{
		public string pageName { get; set; }
		public string slideRightImage { get; set; }
		public string slideContent { get; set; }
		public IList<BeyondCharacterModel> characters { get; set; }
		public IList<string> screenshots { get; set; }
		public IList<string> conceptart { get; set; }
		public string copyrightBox { get; set; }
		public string copyrightInformation { get; set; }
	}
}

