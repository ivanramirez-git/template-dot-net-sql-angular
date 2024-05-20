import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CatTipoCliente } from '../models/cat-tipo-cliente/cat-tipo-cliente';
import { CatTipoClienteGateway } from '../models/cat-tipo-cliente/gateway/cat-tipo-cliente-gateway';

@Injectable({
  providedIn: 'root',
})
export class CatTipoClienteUseCases {
  constructor(private catTipoClienteGateway: CatTipoClienteGateway) {}

  getAllCatTiposClientes(): Observable<CatTipoCliente[]> {
    return this.catTipoClienteGateway.getAll();
  }

  getCatTipoClienteById(id: number): Observable<CatTipoCliente> {
    return this.catTipoClienteGateway.getByID(id);
  }

  createCatTipoCliente(catTipoCliente: CatTipoCliente): Observable<CatTipoCliente> {
    return this.catTipoClienteGateway.saveNew(catTipoCliente);
  }
}
