using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CommonBasicLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace CommonBasicLibraries.CollectionClasses
{
    //i like the idea this time of leaving out the custom.
    public class BasicList<T> : IEnumerable<T>, IListModifiers<T>, ICountCollection, ISimpleList<T>, IBasicList<T> //needs inheritance still because game package needs it.
    {
        protected List<T> PrivateList; //was going to use ilist but we need features that only applies to the ilist.
        public BasicList(int initCapacity = 5)
        {

            PrivateList = new(initCapacity);
            LoadBehavior();
            //hopefully no need for factories.
        } //will go ahead and always use the simple one unless another one is used

        public BasicList(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new CustomArgumentException();
                //throw new ArgumentNullException("CustomBasicList");
            }
            //FactoryRequested = new SimpleCollectionFactory<T>
            //{
            //    SendingType = GetType()
            //};
            PrivateList = new List<T>(list.Count()); //telling them we know what to start with if sending a new list.
            CopyFrom(list);
            LoadBehavior();
            Behavior!.LoadStartLists(list);
        }


        protected bool IsStart = true;

        protected IListModifiers<T>? Behavior;


        protected virtual void LoadBehavior()
        {
            Behavior = new BlankListBehavior<T>(); //so inherited version can load a different behavior.
        }


        protected void CopyFrom(IEnumerable<T> collection) //done
        {
            IList<T> items = PrivateList;
            if (collection != null && items != null)
            {
                using IEnumerator<T> enumerator = collection.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    items.Add(enumerator.Current);
                }
            }
        }
        public int Capacity { get => PrivateList.Capacity; set => PrivateList.Capacity = value; } //now have a new function.
        public void TrimExcess()
        {
            PrivateList.TrimExcess();
        }

        public T this[int index] //not sure how changing this would affect things.
        {
            get { return PrivateList[index]; }
            set
            {

                if (index < 0 || index >= PrivateList.Count)
                {
                    throw new CustomArgumentException("Index", "When setting custom collection, out of range");
                }
                PrivateList[index] = value;

            }
        }
        public int Count => PrivateList.Count; //done

        internal IRandomGenerator? _rs;
        //this needs its own copy somehow.

        //no need for maincontainer now.
        //if a person will not implement it and send in, the system will do it.

        //no need for extras since no collections anymore.


        public void Add(T value) //done i think
        {
            PrivateList.Add(value); //hopefully this simple now.
            Behavior!.Add(value); //we do need behaviors now.
        }

        //because i really don't have to have 2 different interfaces.  that causes problems too.
        public void AddRange(IEnumerable<T> thisList) //done
        {
            PrivateList.AddRange(thisList);
            Behavior!.AddRange(thisList);
        }
        public void Clear()
        {
            PrivateList.Clear();
            Behavior!.Clear();
        }
        public bool Contains(T item) //this uses the proxy because i am controlling access to the list instead of accessing it directly. done
        {
            return PrivateList.Contains(item);
        }
        public bool Exists(Predicate<T> match) //done
        {
            return PrivateList.Exists(match);
        }
        //now it can be null.
        public T? Find(Predicate<T> match) //done
        {
            return PrivateList.Find(match);
        }
        //attempt to not even have findall.  if needed, rethink.


        public IBasicList<T> FindAll(Predicate<T> match) //done
        {
            BasicList<T> output = new();
            foreach (T thisItem in PrivateList)
            {
                if (match(thisItem) == true)
                {
                    output.Add(thisItem);
                }    
            }
            return output; //surprisingly this worked out well.
        }
        public T FindOnlyOne(Predicate<T> match)
        {
            var thisList = FindAll(match);
            if (thisList.Count > 1)
            {
                throw new CustomBasicException("Found more than one item using FindOnlyOne");
            }
            if (thisList.Count == 0)
            {
                throw new CustomBasicException("Did not find any items using FindOnlyOne");
            }
            return thisList.Single();
        }
        public int FindFirstIndex(Predicate<T> match) //done
        {
            return PrivateList.FindIndex(match);
        }
        public int FindFirstIndex(int startIndex, Predicate<T> match) //done
        {
            return PrivateList.FindIndex(startIndex, match);
        }
        public int FindFirstIndex(int startIndex, int count, Predicate<T> match) //done
        {
            return PrivateList.FindIndex(startIndex, count, match);
        }
        public T? FindLast(Predicate<T> match) //done
        {
            return PrivateList.FindLast(match);
        }
        public int FindLastIndex(Predicate<T> match) //done
        {
            return PrivateList.FindLastIndex(match);
        }
        public int FindLastIndex(int startIndex, Predicate<T> match) //done
        {
            return PrivateList.FindLastIndex(startIndex, match);
        }
        public int FindLastIndex(int startIndex, int count, Predicate<T> match) //done
        {
            return PrivateList.FindLastIndex(startIndex, count, match);
        }
        public int LastIndexOf(T thisItem)
        {
            return PrivateList.LastIndexOf(thisItem);
        }
        public void ForEach(Action<T> action) //this is easy because i hook into the list  (done)
        {
            PrivateList.ForEach(action);
        }
        public bool ForSpecificItem(Predicate<T> match, Action<T> action, int howManyToCheck = 0)
        {
            int privateCheck;
            if (howManyToCheck == 0)
            {
                privateCheck = PrivateList.Count;
            }
            else
            {
                privateCheck = howManyToCheck;
            }
            int privateCount = 0;
            foreach (T thisItem in PrivateList)
            {
                privateCount++;
                if (privateCount > privateCheck)
                {
                    break;
                }
                if (match.Invoke(thisItem) == true)
                {
                    action.Invoke(thisItem);
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match1">This is the criteria for doing all</param>
        /// <param name="allAction">This will be performed for all matching criteria</param>
        /// <param name="match2">This is the second criteria</param>
        /// <param name="specificAction">This is the specific action.  The first match it finds, performs the actions and stops</param>
        public void ComplexAction(Predicate<T> match1, Action<T> allAction, Predicate<T> match2, Action<T> specificAction)
        {
            foreach (T thisItem in PrivateList)
            {
                if (match1.Invoke(thisItem) == true)
                    //Console.WriteLine($"Trying To Invoke For Item {ThisItem.ToString()}");
                    allAction.Invoke(thisItem);
            }
            foreach (T thisItem in PrivateList)
            {
                if (match1.Invoke(thisItem) == true && match2.Invoke(thisItem) == true)
                {
                    specificAction.Invoke(thisItem);
                    return; //at this point, can stop because it already found the second match and did the second action.
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match">Condition that must be matched in order to do something</param>
        /// <param name="action">Action to perform on the conditional items</param>
        public void ForConditionalItems(Predicate<T> match, Action<T> action)
        {
            foreach (T thisItem in PrivateList)
            {
                if (match.Invoke(thisItem) == true)
                    action.Invoke(thisItem);
            }
        }
        public async Task ForConditionalItemsAsync(Predicate<T> match, ActionAsync<T> action)
        {
            foreach (T thisItem in PrivateList)
            {
                if (match.Invoke(thisItem) == true)
                    await action.Invoke(thisItem);
            }
        }
        public async Task ForEachAsync(ActionAsync<T> action) //i think done.
        {
            foreach (T thisItem in PrivateList)
            {
                await action.Invoke(thisItem);
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return PrivateList.GetEnumerator();
        }
        public T GetRandomItem() //okay.
        {
            return GetRandomItem(false);
        }
        public T GetRandomItem(bool removePrevious)
        {

            _rs = RandomHelpers.GetRandomGenerator();
            int ask1 = _rs.GetRandomNumber(PrivateList.Count);

            T output = PrivateList[ask1 - 1];
            if (removePrevious)
            {
                RemoveItem(ask1 - 1);
            }
            return output;
        }
        public int GetSeed()
        {
            if (_rs == null)
            {
                //get new random function.

            }
            return _rs!.GetSeed();
        }
        //for getting randomlist, 
        public IBasicList<T> GetRandomList(bool removePrevious, int howManyInList) //done
        {
            _rs = RandomHelpers.GetRandomGenerator();
            BasicList<int> rList = _rs.GenerateRandomList(PrivateList.Count, howManyInList);
            BasicList<T> output = new();
            foreach (var index in rList)
            {
                output.Add(PrivateList[index - 1]);
            }
            if (removePrevious == false)
            {
                return output;
            }
            RemoveGivenList(output);
            return output;
        }
        public void RemoveRandomItems(int howMany)
        {
            _rs = RandomHelpers.GetRandomGenerator();
            BasicList<int> rList = _rs.GenerateRandomList(PrivateList.Count, howMany);
            List<T> list = new ();
            foreach (int index in rList)
            {
                list.Add(PrivateList[index - 1]);
            }
            RemoveGivenList(list);
        }
        public IBasicList<T> GetConditionalItems(Predicate<T> match)
        {
            BasicList<T> output = new();
            foreach (var item in PrivateList)
            {
                if (match.Invoke(item))
                {
                    output.Add(item);
                }
            }
            return output;
        }
        public IBasicList<T> GetRandomList()
        {
            return GetRandomList(false, PrivateList.Count);
        }
        public IBasicList<T> GetRandomList(bool removePrevious)
        {
            return GetRandomList(removePrevious, PrivateList.Count);
        }
        public IBasicList<T> GetRange(int index, int count)
        {
            BasicList<T> output = new();
            for (int i = index; i < count; i++)
            {
                output.Add(PrivateList[i]);
            }
            return output;
        }
        public int IndexOf(T value)
        {
            return PrivateList.IndexOf(value);
        }
        public int IndexOf(T value, int index)
        {
            return PrivateList.IndexOf(value, index);
        }
        public int IndexOf(T value, int index, int count)
        {
            return PrivateList.IndexOf(value, index, count);
        }
        public void InsertBeginning(T value)
        {
            InsertItem(0, value);
        }
        public void InsertMiddle(int index, T value)
        {
            InsertItem(index, value);
        }
        private void InsertItem(int index, T value)
        {
            PrivateList.Insert(index, value);
            Behavior!.Add(value);
        }
        public IBasicList<T> RemoveAllAndObtain(Predicate<T> match)
        {

            BasicList<T> output = new();
            foreach (var item in PrivateList)
            {
                if (match(item))
                {
                    output.Add(item);
                }
            }
            if (output.Count > 0)
            {
                RemoveGivenList(output);
            }
            return output;
        }
        public void RemoveAllOnly(Predicate<T> match)
        {
            List<T> tempList = new ();
            foreach (T item in PrivateList)
            {
                if (match(item) == true)
                {
                    tempList.Add(item);
                }
            }
            if (tempList.Count > 0)
            {
                RemoveGivenList(tempList);
            }
        }
        public void KeepConditionalItems(Predicate<T> match)
        {
            List<T> tempList = new ();
            foreach (T item in PrivateList)
            {
                if (match(item) == false)
                {
                    tempList.Add(item);
                }
            }
            if (tempList.Count > 0)
            {
                RemoveGivenList(tempList);
            }
        }
        public void RemoveAt(int index)
        {
            RemoveItem(index);
        }
        public void RemoveFirstItem()
        {
            RemoveItem(0);
        }

        private void RemoveItem(int index)
        {
            T oldItem = PrivateList[index];
            PrivateList.RemoveAt(index);
            Behavior!.RemoveSpecificItem(oldItem);
        }

        public void RemoveGivenList(IEnumerable<T> list)
        {




            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            foreach (var item in list)
            {
                PrivateList.Remove(item);
                Behavior!.RemoveSpecificItem(item);
            }
            //hopefully this simple now.
        }


        //hopefully no need for factories anymore.  especially since there is no observablecollections anymore.


        public void RemoveLastItem()
        {
            RemoveItem(PrivateList.Count - 1);
        }
        public void RemoveRange(int index, int count)
        {
            List<T> newList = PrivateList.GetRange(index, count);
            RemoveGivenList(newList);
        }
        public bool RemoveSpecificItem(T value)
        {
            int index = PrivateList.IndexOf(value);
            if (index == -1)
                return false; //because not even there.
            RemoveItem(index);
            return true;
        }
        public void ReplaceAllWithGivenItem(T value) //hopefully no problem here (?)  i think done.
        {
            PrivateList.Clear();
            Behavior!.Clear();
            Add(value);
        }
        public void ReplaceItem(T oldItem, T newItem) //i think done
        {
            int index = PrivateList.IndexOf(oldItem);
            RemoveItem(index);
            InsertMiddle(index, newItem); //hopefully this simple.
        }
        public void Reverse() //i think
        {
            PrivateList.Reverse();
        }
        public void ShuffleList()
        {
            if (Count == 0)
            {
                return; //because there is nothing to shuffle.  so can't obviously.  better than runtime error.
            }
            _rs = RandomHelpers.GetRandomGenerator();
            BasicList<int> thisList = _rs.GenerateRandomList(PrivateList.Count); //i think
            List<T> rList = new (); //since they removed and added, then i think its best if i just remove the entire thing.   however, let them know it really moved.
            foreach (int index in thisList)
            {
                rList.Add(PrivateList[index - 1]); //because 0 based.
            }
            PrivateList.Clear();
            PrivateList.AddRange(rList);
        }
        public void ShuffleList(int howMany)
        {
            _rs = RandomHelpers.GetRandomGenerator();
            BasicList<int> thisList = _rs.GenerateRandomList(PrivateList.Count, howMany);
            List<T> rList = new ();
            foreach (int index in thisList)
            {
                rList.Add(PrivateList[index - 1]); //because 0 based.
            }
            PrivateList.Clear();
            InsertRange(0, rList); //i think this time, it will reset it.
        }

        public void Sort()
        {
            PrivateList.Sort();
        }

        public void Sort(Comparison<T> comparison)
        {
            PrivateList.Sort(comparison);
        }
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            PrivateList.Sort(index, count, comparer);
        }
        //for icomparer, 1 means greater than.  -1 means less than.  0 means equal.
        public void Sort(IComparer<T> comparer)
        {
            PrivateList.Sort(comparer);
        }
        public bool TrueForAll(Predicate<T> match)
        {
            return PrivateList.TrueForAll(match);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return PrivateList.GetEnumerator();
        }
        public void LoadStartLists(IEnumerable<T> thisList)
        {
            CopyFrom(thisList);
        }

        public void InsertRange(int index, IEnumerable<T> items) //done i think.
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            PrivateList.InsertRange(index, items);
            Behavior!.AddRange(items);
        }
        public int HowMany(Predicate<T> match)
        {
            int y = 0;
            PrivateList.ForEach(items =>
            {
                if (match.Invoke(items) == true)
                {
                    y++;
                }
            }
            );
            return y;
        }
        public void ReplaceRange(IEnumerable<T> thisList)
        {
            if (thisList == null)
            {
                throw new CustomArgumentException("Collection Cannot Be Nothing When Replacing Range");
            }
            PrivateList.Clear();
            PrivateList.AddRange(thisList); //i think
            Behavior!.ReplaceRange(thisList);
        }
        public void RemoveOnlyOneAfterAction(Predicate<T> match, Action<T> action) //does not have to be there.  if not there, ignore
        {
            if (Exists(match) == false)
            {
                return; //because there is none.
            }
            T thisItem;
            try
            {
                thisItem = FindOnlyOne(match); //so if more than one is found, then will raise an exception
            }
            catch (CustomBasicException)
            {
                throw new CustomBasicException("RemoveOnlyOneAction Had An Error.  Most Likely, The Condition Had More Than Element Satisfying It");
            }
            catch (Exception ex)
            {
                throw new CustomBasicException($"Other Exception Was Thrown.  The Error Was {ex.Message}");
            }
            action.Invoke(thisItem);
            RemoveSpecificItem(thisItem);
        }
        public void RemoveSeveralConditionalItems(BasicList<ConditionActionPair<T>> thisList)
        {
            BasicList<T> rList = new ();
            thisList.ForEach(firstItem =>
            {
                if (Exists(firstItem.Predicate) == true)
                {
                    T thisItem;
                    try
                    {
                        thisItem = FindOnlyOne(firstItem.Predicate);
                    }
                    catch (CustomBasicException)
                    {
                        throw new CustomBasicException("RemoveSeveralConditionalItems Had An Error.  Most Likely, The Condition Had More Than Element Satisfying One Of The Condition Lists");
                    }
                    catch (Exception ex)
                    {
                        throw new CustomBasicException($"Other Exception Was Thrown.  The Error Was {ex.Message}");
                    }
                    firstItem.Action.Invoke(thisItem, firstItem.Value);
                    rList.Add(thisItem);
                }
            });
            RemoveGivenList(rList);
        }
        //ConvertAll  try to not have convertall anymore.  if i am wrong, rethink.

        public IBasicList<U> ConvertAll<U>(Converter<T, U> converter)
        {
            BasicList<U> output = new();
            output.Capacity = PrivateList.Count; //use their count
            for (int i = 0; i < PrivateList.Count; i++)
            {
                output.Add(converter(PrivateList[i]));
            }
            return output;
        }

        public void ReplaceWithNewObjects(int howMany, Func<T> func)
        {
            BasicList<T> newList = new();
            howMany.Times(() =>
            {
                newList.Add(func.Invoke());
            });
            ReplaceRange(newList);
        }
        public void ReplaceWithNewObjects(Func<T> func)
        {
            ReplaceWithNewObjects(Count, func);
        }
        public void MoveItem(T item, int newIndex)
        {
            int oldIndex = PrivateList.IndexOf(item);
            PrivateList.RemoveAt(oldIndex);
            PrivateList.Insert(newIndex, item);
        }



    }
}
