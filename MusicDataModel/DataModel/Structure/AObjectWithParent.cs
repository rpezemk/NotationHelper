using MusicDataModel.DataModel.Elementary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModel.DataModel.Structure
{
    public interface IChild
    {
        public ObjectTypeEnum ParentType { get; }
        public IParentOf<IChild> GetParentObj();
        
    }

    public interface IChild<TParent> : IChild
    {
        public void SetParent(TParent obj);

        public bool TryGetNext(out IChild<TParent> next);
        public TParent GetParentT();
    }
    
    public interface IParentOf<TChild>
    {
        public void AppendChild(TChild child);
        public void ReplaceChild(TChild oldChild, TChild newChild);
        public List<TChild> Children { get; }
    }


    public abstract class AObjectWithChildren<TObject, TChild> : IParentOf<TChild>
        where TObject : class, IParentOf<TChild>
        where TChild : IChild<TObject>
    {
        private List<TChild> children = new List<TChild>();
        public List<TChild> Children => children;
        public void AppendChild(TChild child)
        {
            Children.Add(child);
            child.SetParent(ThisObj);
        }
        public AObjectWithChildren()
        {

        }
        public void ReplaceChild(TChild child, TChild newChild)
        {
            var idx = Children.IndexOf(child);
            if(idx != -1)
            {
                Children[idx] = newChild;
                newChild.SetParent(ThisObj);
            }
        }

        public abstract TObject ThisObj { get; }
    }

    public abstract class AObjectWithParent<TParent, TObject> : IChild<TParent>
        where TParent : IParentOf<TObject>, new ()
        where TObject: class
    {
        public AObjectWithParent()
        {
            ThisObject = this as TObject;
        }
        public TParent Parent { get; set; }

        public abstract ObjectTypeEnum ParentType { get; }

        public TObject ThisObject;

        public void SetParent(TParent obj)
        {
            Parent = obj;
        }

        public bool TryGetNext(out IChild<TParent> next)
        {
            next = null;
            var idx = Parent.Children.IndexOf(this as TObject);
            if(idx < Parent.Children.Count - 1)
            {
                next = Parent.Children[idx + 1] as IChild<TParent>;
                return true;
            }
            else if (Parent is IChild otherParentChild)
            {
                var granpa = otherParentChild.GetParentObj();
                var idx2 = granpa.Children.IndexOf(otherParentChild);
                if(idx2 < granpa.Children.Count-1)
                {
                    var parentSybling = granpa.Children[idx2 + 1];
                }
                return true;
            }
            

            return false;
        }

        public TParent GetParentT()
        {
            return Parent;
        }

        public IParentOf<IChild> GetParentObj()
        {
            throw new NotImplementedException();
        }
    }



    public abstract class AObjectWithParentAndChildren<TParent, TObject, TChild> : AObjectWithParent<TParent, TObject>, IChild<TParent>, IParentOf<TChild>
        where TChild : IChild<TObject>
        where TObject: class, IParentOf<TChild>
        where TParent : IParentOf<TObject>, new()
    {

        public List<TChild> children = new List<TChild>();
        public List<TChild> Children => children;
        public TObject ThisObject;
        public AObjectWithParentAndChildren()
        {
            ThisObject = this as TObject;
        }
        public void AppendChild(TChild child)
        {
            Children.Add(child);
            child.SetParent(ThisObject);
        }

        public void ReplaceChild(TChild oldChild, TChild newChild)
        {
            var idx = Children.IndexOf(oldChild);
            if (idx != -1)
            {
                Children[idx] = newChild;
                newChild.SetParent(ThisObject);
            }
        }
    }
}
