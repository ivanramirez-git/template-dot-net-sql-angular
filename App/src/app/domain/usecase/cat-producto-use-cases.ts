import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CatProducto } from '../models/cat-producto/cat-producto';
import { CatProductoGateway } from '../models/cat-producto/gateway/cat-producto-gateway';

@Injectable({
  providedIn: 'root',
})
export class CatProductoUseCases {
  constructor(private catProductoGateway: CatProductoGateway) {}

  getAllCatProductos(): Observable<CatProducto[]> {
    return this.catProductoGateway.getAll();
  }

  getCatProductoById(id: number): Observable<CatProducto> {
    return this.catProductoGateway.getByID(id);
  }

  createCatProducto(catProducto: CatProducto): Observable<CatProducto> {
    return this.catProductoGateway.saveNew(catProducto);
  }
}
