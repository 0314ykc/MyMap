namespace MyMap.Models
{
    public enum MapType
    {
        others = 0,
        food = 1,
        /// <summary>
        /// ���I
        /// </summary>
        attractions = 2
    }

    public enum MapPlaceType
    {
        /// <summary>
        /// ������
        /// </summary>
        uncategorized = 0,
        food = 1,
        /// <summary>
        /// ���I
        /// </summary>
        attractions = 2
    }

    /// <summary>
    /// �ਣ��
    /// </summary>
    public enum visibility
    {
        /// <summary>
        /// �Ҧ��H���i�H��
        /// </summary>
        publicV = 0,
        /// <summary>
        /// �u���ۤv
        /// </summary>
        prviateV = 1,
        /// <summary>
        /// �֦�Url���i�H��
        /// </summary>
        haveUrlOnlyV = 2,
    }

    public enum pageHeaderType
    {
        defaultLayout = 0,
        MyMap = 1,

    }
}