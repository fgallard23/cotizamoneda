import {HttpClient, HttpHeaders} from "@angular/common/http";
import { Injectable } from "@angular/core";
// rxjs
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import {Moneda} from "../model/moneda";

@Injectable({
  providedIn: 'root'
})
export class MonedaService{
  private cotizacionUrl = 'https://www.bancoprovincia.com.ar/Principal/Dolar';
  private comprarUrl="/api/Moneda/ComprarMoneda"

  constructor(private http: HttpClient){ }

  // cotizar
  getCotizacion(): Observable<string[]>{
    return this.http.get<string[]>(this.cotizacionUrl).pipe(
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  // comprar
  createCompra(moneda:Moneda):Observable<Moneda>{
    let httpheaders=new HttpHeaders().set('Content-type','application/Json');
    let options={ headers:httpheaders };
    console.log(moneda);
    return this.http.post<Moneda>(this.comprarUrl, moneda, options);
  }

  private handleError(err: { error: { message: any; }; status: any; body: { error: any; }; }) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }
}
