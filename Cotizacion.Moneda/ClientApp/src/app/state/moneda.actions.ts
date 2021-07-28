import { Action } from "@ngrx/store";
import { Moneda } from "../model/moneda";
import { MonedaActionTypes } from "./moneda.enum";

export class Cotizar implements Action {
  readonly type = MonedaActionTypes.Cotizar;
  constructor(public payload: string []) { }
}

export class Comprar implements Action {
  readonly type = MonedaActionTypes.Comprar;
  constructor(public payload: Moneda) { }
}

export class LoadFail implements Action {
  readonly type = MonedaActionTypes.LoadFail;
  constructor(public payload: string) { }
}

export type MonedaActions = Cotizar | Comprar | LoadFail;

