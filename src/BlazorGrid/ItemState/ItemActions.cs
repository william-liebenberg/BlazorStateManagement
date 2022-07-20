using Shared;

namespace BlazorGrid.ItemState
{
    public static class ItemActions
    {
        public record AddItemView(ItemView item);
        public record ClearItem;
    }
}