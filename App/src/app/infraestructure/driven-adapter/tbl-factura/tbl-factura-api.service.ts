import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigService } from '../config.service';
import { TblFacturaGateway } from '../../../domain/models/tbl-factura/gateway/tbl-factura-gateway';
import { TblFactura } from '../../../domain/models/tbl-factura/tbl-factura';

@Injectable({
  providedIn: 'root',
})
export class TblFacturaApiService extends TblFacturaGateway {
  private _url = `${this.config.apiUrl}/TblFactura`;

  constructor(private http: HttpClient, private config: ConfigService) {
    super();
  }

  getByID(id: number): Observable<TblFactura> {
    return this.http.get<TblFactura>(`${this._url}/${id}`);
  }

  getAll(): Observable<TblFactura[]> {
    return this.http.get<TblFactura[]>(this._url);
  }

  saveNew(tblFactura: TblFactura): Observable<TblFactura> {
    // Implementa la lógica para guardar una nueva TblFactura
    // y devuelve un observable si es necesario.
    return this.http.post<TblFactura>(this._url, tblFactura);
  }

  getByClienteId(idCliente: number): Observable<TblFactura[]> {
    // Implementa la lógica para obtener las facturas de un cliente
    // y devuelve un observable si es necesario.
    return this.http.get<TblFactura[]>(`${this._url}/TblCliente/${idCliente}`);
  }

  getByNumeroFactura(numeroFactura: number): Observable<TblFactura[]> {
    // Implementa la lógica para obtener las facturas por numero
    // y devuelve un observable si es necesario.
    return this.http.get<TblFactura[]>(`${this._url}/NumeroFactura/${numeroFactura}`);
  }
}
