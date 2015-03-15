using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback
{
	public class MidiStreamEventMerger
	{
		public static IEnumerable<IMidiStreamEvent> Merge(IEnumerable<IEnumerable<IMidiStreamEvent>> tracks)
		{
			var mergeCollection = new MergeCollection();

			var enumerators = tracks.Select(x => x.GetEnumerator()).ToList();
			var lastEventPositions = enumerators.Select(x => 0).ToList();

			foreach (var t in tracks)
			{
				long absolute = 0;
				foreach (var e in t)
				{
					absolute += e.DeltaTime;
					mergeCollection.Insert(e, absolute);
				}
			}

			return mergeCollection.Events;
		}

		private class MergeCollection
		{
			private readonly LinkedList<IMidiStreamEvent> events = new LinkedList<IMidiStreamEvent>();

			public IEnumerable<IMidiStreamEvent> Events { get { return this.events; } }

			public void Insert(IMidiStreamEvent e, long absolute)
			{
				var node = this.events.First;
				long currentAbsolute = 0;
				while (node != null)
				{
					currentAbsolute += node.Value.DeltaTime;
					var nextNode = node.Next;
					if (nextNode != null && currentAbsolute + nextNode.Value.DeltaTime > absolute)
					{
						// insert note after the current node
						// and adjust the next node
						var added = this.events.AddAfter(node, e.Clone((uint)(absolute - currentAbsolute)));
						// adjust the next node
						var nextNodeDeltaTime = currentAbsolute + nextNode.Value.DeltaTime - absolute;
						this.events.Remove(nextNode);
						this.events.AddAfter(added, nextNode.Value.Clone((uint)nextNodeDeltaTime));
						return;
					}

					node = node.Next;
				}
				this.events.AddLast(e.Clone((uint)(absolute - currentAbsolute)));
			}
		}
	}
}
