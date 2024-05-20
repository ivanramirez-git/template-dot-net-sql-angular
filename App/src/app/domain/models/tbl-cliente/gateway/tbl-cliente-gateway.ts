// gateway/tbl-cliente-gateway.ts

import { Observable } from 'rxjs';
import { TblCliente } from '../tbl-cliente';

export abstract class TblClienteGateway {
  abstract getByID(id: number): Observable<TblCliente>;
  abstract getAll(): Observable<Array<TblCliente>>;
  abstract saveNew(cliente: TblCliente): Observable<TblCliente>;

}
