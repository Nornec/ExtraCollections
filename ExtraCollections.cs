public static class ExtraCollections
{
  /// <summary>
  /// Combines the powers of a LinkedList and a HashSet to ensure uniqueness of values within a LinkedList.
  /// </summary>
  /// <typeparam name="T">The type of object in which the UniqueList will be based</typeparam>
  public class UniqueList<T>
  {
    private readonly LinkedList<T> list;
    private readonly HashSet<T> hash;

    public UniqueList()
    {
      list = new LinkedList<T>();
      hash = new HashSet<T>();
    }
    public UniqueList(UniqueList<T> in_list)
    { 
      list = new LinkedList<T>(in_list.list);
      hash = new HashSet<T>(in_list.hash);
    }

    //Extensions on the enumerable classes underneath
    public int Count => hash.Count;
    public bool Any => hash.Any();

    public void Add(T item)
    {
      if (hash.Contains(item))
      {
        // Item already exists in the list
        return;
      }

      list.AddLast(item);
      hash.Add(item);
    }

    public bool Remove(T item)
    {
      if (!hash.Contains(item))
      {
        // Item doesn't exist in the list and was not removed. Returns false.
        return false;
      }

      list.Remove(item);
      hash.Remove(item);
      return true;
    }

    public void Clear()
    {
      list.Clear();
      hash.Clear();
    }

    public T First => list.ElementAt(0);

    public List<T> ToList => list.ToList();

    public UniqueList<T> Copy => new(this);

    public List<T> Concat(UniqueList<T> next_list)
    {
      List<T> new_list = next_list.ToList;
      return list.Concat(new_list).ToList();
    }

    public T ElementAt(int index) 
    {
      return list.ElementAt(index);
    }

    public bool Contains(T item)
    {
      return hash.Contains(item);
    }

    public void MoveAfter(T move_this, T after_this)
    {
      LinkedListNode<T> node = list.Find(move_this);
      LinkedListNode<T> target = list.Find(after_this);

      // remove node from its current position in the list
      Remove(move_this);

      // insert node after targetNode
      list.AddAfter(target, node);
      hash.Add(move_this);
    }

    public void MoveBefore(T move_this, T before_this)
    {
      LinkedListNode<T> node = list.Find(move_this);
      LinkedListNode<T> target = list.Find(before_this);
      // remove node from its current position in the list
      Remove(move_this);

      // insert node before targetNode
      list.AddBefore(target, node);
      hash.Add(move_this);
    }

    public void MoveFirst(T item)
    {
      // remove node from its current position in the list
      Remove(item);

      // insert node at the beginning of the list
      list.AddFirst(item);
      hash.Add(item);
    }

    public void MoveLast(T item)
    {
      // remove item from its current position in the list
      Remove(item);

      // insert node at the end of the list
      Add(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
      return list.GetEnumerator();
    }
  }
}
