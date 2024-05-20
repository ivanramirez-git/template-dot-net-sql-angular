// gateway/cat-producto-gateway.ts

import { Observable } from 'rxjs';
import { CatProducto } from '../cat-producto';

export abstract class CatProductoGateway {
  abstract getByID(id: number): Observable<CatProducto>;
  abstract getAll(): Observable<Array<CatProducto>>;
  abstract saveNew(catProducto: CatProducto): Observable<CatProducto>;
}
