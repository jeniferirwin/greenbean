using UnityEngine;
using System.Collections.Generic;

namespace Com.Technitaur.GreenBean.Graphics
{
    public static class VICEPalette
    {
        public static Color[] colors = {
        	new Color(0.000f, 0.000f, 0.000f, 1.000f),
        	new Color(1.000f, 1.000f, 1.000f, 1.000f),
        	new Color(0.408f, 0.216f, 0.169f, 1.000f),
        	new Color(0.439f, 0.643f, 0.698f, 1.000f),
        	new Color(0.435f, 0.239f, 0.525f, 1.000f),
        	new Color(0.345f, 0.553f, 0.263f, 1.000f),
        	new Color(0.208f, 0.157f, 0.475f, 1.000f),
        	new Color(0.722f, 0.780f, 0.435f, 1.000f),
        	new Color(0.435f, 0.310f, 0.145f, 1.000f),
        	new Color(0.263f, 0.224f, 0.000f, 1.000f),
        	new Color(0.604f, 0.404f, 0.349f, 1.000f),
			new Color(0.267f, 0.267f, 0.267f, 1.000f),
        	new Color(0.424f, 0.424f, 0.424f, 1.000f),
        	new Color(0.604f, 0.824f, 0.518f, 1.000f),
        	new Color(0.424f, 0.369f, 0.710f, 1.000f),
        	new Color(0.584f, 0.584f, 0.584f, 1.000f)
        };
		
		public static Dictionary<int,Color[]> schemes = new Dictionary<int, Color[]>()
		{
			{ 1,  new Color[] { colors[10], colors[2]   } },
			{ 2,  new Color[] { colors[9],  colors[5]   } },
			{ 3,  new Color[] { colors[9],  colors[3]   } },
			{ 4,  new Color[] { colors[9],  colors[4]   } },
			{ 5,  new Color[] { colors[8],  colors[9]   } },
			{ 6,  new Color[] { colors[9],  colors[12]  } },
			{ 7,  new Color[] { colors[2],  colors[4]   } },
			{ 8,  new Color[] { colors[2],  colors[14]  } },
			{ 9,  new Color[] { colors[12], colors[11]  } },
			{ 10, new Color[] { colors[12], colors[6]   } },
		};
    }
}