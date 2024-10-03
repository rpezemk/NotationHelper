using MusicDataModel.DataModel.Elementary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModel.DataModel.Structure
{
    public interface IChildOf
    {
        public ObjectTypeEnum ParentType { get; }
        
    }

    public interface IChildOf<TParent> : IChildOf
    {
        public void SetParent(TParent obj);
    }
    
    public interface IParentOf<TChild>
    {
        public void AppendChild(TChild child);
        public void ReplaceChild(TChild oldChild, TChild newChild);
    }


    public abstract class AObjectWithChildren<TObject, TChild> : IParentOf<TChild>
        where TObject : class, IParentOf<TChild>
        where TChild : IChildOf<TObject>
    {
        public List<TChild> Children = new List<TChild>();
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

    public abstract class AObjectWithParent<TParent, TObject> : IChildOf<TParent>
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
    }



    public abstract class AObjectWithParentAndChildren<TParent, TObject, TChild> : AObjectWithParent<TParent, TObject>, IChildOf<TParent>, IParentOf<TChild>
        where TChild : IChildOf<TObject>
        where TObject: class, IParentOf<TChild>
        where TParent : IParentOf<TObject>, new()
    {

        public List<TChild> Children = new List<TChild>();
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
