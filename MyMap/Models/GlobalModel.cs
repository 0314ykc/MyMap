namespace MyMap.Models
{
    public enum MapType
    {
        others = 0,
        food = 1,
        /// <summary>
        /// 景點
        /// </summary>
        attractions = 2
    }

    public enum MapPlaceType
    {
        /// <summary>
        /// 未分類
        /// </summary>
        uncategorized = 0,
        food = 1,
        /// <summary>
        /// 景點
        /// </summary>
        attractions = 2
    }

    /// <summary>
    /// 能見度
    /// </summary>
    public enum visibility
    {
        /// <summary>
        /// 所有人都可以看
        /// </summary>
        publicV = 0,
        /// <summary>
        /// 只有自己
        /// </summary>
        prviateV = 1,
        /// <summary>
        /// 擁有Url的可以看
        /// </summary>
        haveUrlOnlyV = 2,
    }

    public enum pageHeaderType
    {
        defaultLayout = 0,
        MyMap = 1,

    }
}