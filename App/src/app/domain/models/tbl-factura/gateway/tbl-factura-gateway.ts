// gateway/tbl-factura-gateway.ts

import { Observable } from 'rxjs';
import { TblFactura } from '../tbl-factura';

export abstract class TblFacturaGateway {
  abstract getByID(id: number): Observable<TblFactura>;
  abstract getAll(): Observable<Array<TblFactura>>;
  abstract saveNew(factura: TblFactura): Observable<TblFactura>;

  abstract getByClienteId(idCliente: number): Observable<Array<TblFactura>>;
  abstract getByNumeroFactura(numeroFactura: number): Observable<TblFactura[]>;
}
