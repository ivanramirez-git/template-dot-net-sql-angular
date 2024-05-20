import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TblDetalleFactura } from '../models/tbl-detalle-factura/tbl-detalle-factura';
import { TblDetalleFacturaGateway } from '../models/tbl-detalle-factura/gateway/tbl-detalle-factura-gateway';

@Injectable({
  providedIn: 'root',
})
export class TblDetalleFacturaUseCases {
  constructor(private tblDetalleFacturaGateway: TblDetalleFacturaGateway) {}

  getAllTblDetalleFacturas(): Observable<TblDetalleFactura[]> {
    return this.tblDetalleFacturaGateway.getAll();
  }

  getTblDetalleFacturaById(id: number): Observable<TblDetalleFactura> {
    return this.tblDetalleFacturaGateway.getByID(id);
  }

  createTblDetalleFactura(tblDetalleFactura: TblDetalleFactura): Observable<TblDetalleFactura> {
    return this.tblDetalleFacturaGateway.saveNew(tblDetalleFactura);
  }
}
