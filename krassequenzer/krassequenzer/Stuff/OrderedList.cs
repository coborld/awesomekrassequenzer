using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.Stuff
{
	/// <summary>
	/// Represents an automatically ordered collection.
	/// Note that this thing only works if the result of the
	/// comparison predicate is immutable from the time the
	/// item is added.
	/// </summary>
	internal class OrderedCollection<TItem> : ICollection<TItem>
	{
		public OrderedCollection(Comparison<TItem> comparison)
		{
			this.comparison = comparison.NotNull("comparison");
			var comparer = new Comparer(this.comparison);
			this.innerList = new SortedSet<TItem>(comparer);
		}

		private class Comparer : IComparer<TItem>
		{
			public Comparer(Comparison<TItem> comparison)
			{
				this.comparison = comparison;
			}

			private readonly Comparison<TItem> comparison;

			public int Compare(TItem x, TItem y)
			{
				return this.comparison(x, y);
			}
		}

		private readonly Comparison<TItem> comparison;
		private readonly SortedSet<TItem> innerList;

		public void Add(TItem item)
		{
			this.innerList.Add(item);
		}

		public void Clear()
		{
			this.innerList.Clear();
		}

		public bool Contains(TItem item)
		{
			return this.innerList.Contains(item);
		}

		public void CopyTo(TItem[] array, int arrayIndex)
		{
			array.NotNull("array");
			if (array.Length < this.Count + arrayIndex) throw new IndexOutOfRangeException();

			var i = arrayIndex;
			foreach (var item in this.innerList)
			{
				array[i++] = item;
			}
		}

		public int Count
		{
			get { return this.innerList.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(TItem item)
		{
			return this.innerList.Remove(item);
		}

		public IEnumerator<TItem> GetEnumerator()
		{
			return this.innerList.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.innerList.GetEnumerator();
		}
	}
}
