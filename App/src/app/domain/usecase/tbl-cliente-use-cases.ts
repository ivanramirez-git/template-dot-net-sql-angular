import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TblCliente } from '../models/tbl-cliente/tbl-cliente';
import { TblClienteGateway } from '../models/tbl-cliente/gateway/tbl-cliente-gateway';

@Injectable({
  providedIn: 'root',
})
export class TblClienteUseCases {
  constructor(private tblClienteGateway: TblClienteGateway) {}

  getAllTblClientes(): Observable<TblCliente[]> {
    return this.tblClienteGateway.getAll();
  }

  getTblClienteById(id: number): Observable<TblCliente> {
    return this.tblClienteGateway.getByID(id);
  }

  createTblCliente(tblCliente: TblCliente): Observable<TblCliente> {
    return this.tblClienteGateway.saveNew(tblCliente);
  }


}
