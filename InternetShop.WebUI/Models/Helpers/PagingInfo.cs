namespace InternetShop.WebUI.Models
{
    public struct PagingInfo
    {
        public int CurrentPage { get; }
        public int ItemsPerPage { get; }
        public int TotalItems { get; }
        public int TotalPage => (int) System.Math.Ceiling( (double)TotalItems/ItemsPerPage);

        public PagingInfo(int currentPage, int itemsPerPage, int totalItems)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
        }
    }
}