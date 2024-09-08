namespace NotationHelper.DataModel.Structure
{
    public class NoneType : IChildOf
    {
        public ObjectTypeEnum ParentType => ObjectTypeEnum.None;
    }

    public class NoneType<T> : NoneType, IChildOf<T>
    {
        
        private T parent;
        public void SetParent(T obj)
        {
            parent = obj;
        }
    }
}
