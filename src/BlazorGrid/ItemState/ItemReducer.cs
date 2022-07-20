using Fluxor;

namespace BlazorGrid.ItemState
{
    public static class ItemReducer
    {
        [ReducerMethod]
        public static ItemState AddItemView(ItemState state, ItemActions.AddItemView action)
        {
            state.ItemViews.Insert(0, action.item);

            return state with
            {
                ItemViews = state.ItemViews,
            };
        }
        
        [ReducerMethod]
        public static ItemState ClearItem(ItemState state, ItemActions.ClearItem action)
        {
            state.ItemViews.Clear();

            return state with
            {
                ItemViews = state.ItemViews,
            };
        }
    }
}