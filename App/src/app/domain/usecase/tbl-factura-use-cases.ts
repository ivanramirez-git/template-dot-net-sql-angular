import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TblFactura } from '../models/tbl-factura/tbl-factura';
import { TblFacturaGateway } from '../models/tbl-factura/gateway/tbl-factura-gateway';

@Injectable({
  providedIn: 'root',
})
export class TblFacturaUseCases {
  constructor(private tblFacturaGateway: TblFacturaGateway) {}

  getAllTblFacturas(): Observable<TblFactura[]> {
    return this.tblFacturaGateway.getAll();
  }

  getTblFacturaById(id: number): Observable<TblFactura> {
    return this.tblFacturaGateway.getByID(id);
  }

  createTblFactura(tblFactura: TblFactura): Observable<TblFactura> {
    return this.tblFacturaGateway.saveNew(tblFactura);
  }

  getTblFacturasByCliente(id: number): Observable<TblFactura[]> {
    return this.tblFacturaGateway.getByClienteId(id);
  }

  getTblFacturasByNumber(numeroFactura: number): Observable<TblFactura[]> {
    return this.tblFacturaGateway.getByNumeroFactura(numeroFactura);
  }
}
