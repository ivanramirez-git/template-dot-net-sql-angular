// gateway/cat-tipo-cliente-gateway.ts

import { Observable } from 'rxjs';
import { CatTipoCliente } from '../cat-tipo-cliente';

export abstract class CatTipoClienteGateway {
    abstract getByID(id: number): Observable<CatTipoCliente>;
    abstract getAll(): Observable<Array<CatTipoCliente>>;
    abstract saveNew(tipoCliente: CatTipoCliente): Observable<CatTipoCliente>;
}
