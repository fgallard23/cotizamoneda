import {Component, OnInit} from '@angular/core';
import { MonedaState } from "../state/moneda.state";
import { Store } from "@ngrx/store";
import { MonedaService } from "../services/moneda-service";
import * as monedaActions from './../state/moneda.actions';

@Component({
  selector: 'app-cotizar-component',
  templateUrl: './cotizar.component.html',
})
export class CotizarComponent implements OnInit {
  cotizacion: string[];

  constructor(private store: Store<MonedaState>,
              private monedaService: MonedaService ) { }

  ngOnInit() {
    this.store.dispatch(new monedaActions.Cotizar(this.cotizacion));
    this.monedaService.getCotizacion().subscribe(cotizar => this.cotizacion = cotizar)
    console.log(this.cotizacion);
  }

  // function convert Dolar to Real
  convertirReal = (valor: string) => {
    if (Number(valor))
      return parseFloat(valor) /4;
    return valor;
  }
}

