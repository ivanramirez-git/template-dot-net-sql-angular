// gateway/tbl-detalle-factura-gateway.ts

import { Observable } from 'rxjs';
import { TblDetalleFactura } from '../tbl-detalle-factura';

export abstract class TblDetalleFacturaGateway {
  abstract getByID(id: number): Observable<TblDetalleFactura>;
  abstract getAll(): Observable<Array<TblDetalleFactura>>;
  abstract saveNew(detalleFactura: TblDetalleFactura): Observable<TblDetalleFactura>;
}
