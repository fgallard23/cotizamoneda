import { Injectable } from '@angular/core';
// rxjs
import { of } from 'rxjs';
import { map, mergeMap, catchError } from 'rxjs/operators';
// effects
import {Actions, createEffect, ofType} from '@ngrx/effects';
//
import { MonedaActionTypes } from './moneda.enum';
// actions
import * as monedaActions from './moneda.actions';
import { MonedaService } from '../services/moneda-service';

@Injectable()
export class MonedaEffects{

  constructor(
    private monedaService : MonedaService,
    private actions$: Actions
  ){}

  loadHeroes$ = createEffect( () => this.actions$.pipe(
    ofType(MonedaActionTypes.Cotizar),
    mergeMap( () => this.monedaService.getCotizacion().pipe(
      map(cotizar => new monedaActions.Cotizar(cotizar)),
      catchError(err => of(new monedaActions.LoadFail(err)))
    ))));
}
