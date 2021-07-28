import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Moneda} from "../model/moneda";
import {MonedaService} from "../services/moneda-service";

@Component({
  selector: 'app-comprar-component',
  templateUrl: './comprar.component.html',
  styleUrls: ['./comprar.component.css']
})
export class ComprarComponent implements OnInit {
  tipoMonedas: { name: string, id: string }[] = [{ name: "Dolar", id: "DOLAR" },
    { name: "Real", id: "REAL" }];
  datasaved = false;
  monedaForm: FormGroup;

  constructor(private formbuilder: FormBuilder, private monedaservice:MonedaService ) {
  }

  ngOnInit(): void {
    this.monedaForm = this.formbuilder.group({
      idUsuario: ['', [Validators.required]],
      tipoMoneda: ['', [Validators.required]],
      montoComprar: ['', [Validators.required]]
    });
  }

  onFormSubmit() {
    this.datasaved = false;
    let moneda = this.monedaForm.value;
    moneda.tipoMoneda = moneda.tipoMoneda.toUpperCase();
    this.createCompras(moneda);
    this.monedaForm.reset();
  }

  createCompras(moneda: Moneda){
    console.log(this.monedaservice.createCompra(moneda));
    this.datasaved = true;
  }
}

