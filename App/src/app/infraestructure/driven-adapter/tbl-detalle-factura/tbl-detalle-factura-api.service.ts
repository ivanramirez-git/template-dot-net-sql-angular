import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigService } from '../config.service';
import { TblDetalleFacturaGateway } from '../../../domain/models/tbl-detalle-factura/gateway/tbl-detalle-factura-gateway';
import { TblDetalleFactura } from '../../../domain/models/tbl-detalle-factura/tbl-detalle-factura';

@Injectable({
  providedIn: 'root',
})
export class TblDetalleFacturaApiService extends TblDetalleFacturaGateway {
  private _url = `${this.config.apiUrl}/TblDetalleFactura`;

  constructor(private http: HttpClient, private config: ConfigService) {
    super();
  }

  getByID(id: number): Observable<TblDetalleFactura> {
    return this.http.get<TblDetalleFactura>(`${this._url}/${id}`);
  }

  getAll(): Observable<TblDetalleFactura[]> {
    return this.http.get<TblDetalleFactura[]>(this._url);
  }

  saveNew(tblDetalleFactura: TblDetalleFactura): Observable<TblDetalleFactura> {
    // Implementa la lógica para guardar un nuevo TblDetalleFactura
    // y devuelve un observable si es necesario.
    return this.http.post<TblDetalleFactura>(this._url, tblDetalleFactura);
  }
}
