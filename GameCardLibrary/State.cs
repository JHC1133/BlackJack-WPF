using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
	/// <summary>
	/// The possible states of the game's participants
	/// </summary>
	public enum State
	{
		Blackjack,
		Bust,
		Winner,
		Loser,
		Tie
	}
}
