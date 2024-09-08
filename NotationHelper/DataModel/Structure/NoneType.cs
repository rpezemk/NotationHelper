namespace NotationHelper.DataModel.Structure
{
    public class NoneType : IChildOf
    {
        public ObjectTypeEnum ParentType => throw new NotImplementedException();
    }

    public class NoneType<T> : IChildOf<T>
    {
        public ObjectTypeEnum ParentType => throw new NotImplementedException();
        private T parent;
        public void SetParent(T obj)
        {
            parent = obj;
        }
    }
}
