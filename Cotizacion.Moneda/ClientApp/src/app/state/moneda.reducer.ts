import { MonedaActions } from "./moneda.actions";
import { MonedaActionTypes } from "./moneda.enum";
import { MonedaState } from "./moneda.state";

export const initialState: MonedaState = { modena: null, cotizacion : [] };

export function herosReducer(state = initialState, action: MonedaActions): MonedaState {

  switch (action.type) {
    case MonedaActionTypes.Cotizar:
      return {
        ...state,
        cotizacion: action.payload,
      };

    case MonedaActionTypes.Comprar:
      return {
        ...state,
        modena: action.payload,
      };

    case MonedaActionTypes.LoadFail:
      return {
        ...state,
      };

    default:
      return state;
  }
}
