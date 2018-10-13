using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowLabels  {
	public List<OrganelleLabel> OrganelleLabels {get; private set;}

	public LoadGameData.OrganelleData[] OrganelleData;

	// Use this for initialization
	public void Show () {
		LoadOrganelles();

		Debug.Log(string.Format("{0} Organelles Added", OrganelleLabels.Count));
	}
	
	private void LoadOrganelles() {
		if (OrganelleData != null) {
			float ymargin = 0.7F;
			int fontSize = (int)(ymargin*50);
			bool bothSides = (OrganelleData.Length > 10);
			float left = -8, right = 5;
			float x = bothSides ? left : right;
			int divisor = bothSides ? 4 : 2;
			float yTop = ((ymargin * OrganelleData.Length)/divisor) + (OrganelleData.Length % 2);
			float y = yTop;

			OrganelleLabels = new List<OrganelleLabel>(OrganelleData.Select(o => {
				OrganelleLabel organelle = new OrganelleLabel() {
					Name = o.name,
					Loc = new Vector2(x, y),
					Dest = new Vector2(o.destx, o.desty),
					FontSize = fontSize
				};
				y-=ymargin;
				if (y <= -yTop) {
					y = yTop;
					x = right;
				}

				if (x < 0)
					x += (y >= 0) ? -0.1F : 0.1F;
				else
					x += (y >= 0) ? 0.1F : -0.1F;

				return organelle;
			}));
		}
	}
}
