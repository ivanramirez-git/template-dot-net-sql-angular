import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigService } from '../config.service';
import { CatTipoCliente } from '../../../domain/models/cat-tipo-cliente/cat-tipo-cliente';
import { CatTipoClienteGateway } from '../../../domain/models/cat-tipo-cliente/gateway/cat-tipo-cliente-gateway';

@Injectable({
  providedIn: 'root',
})
export class CatTipoClienteApiService extends CatTipoClienteGateway {
  private _url = `${this.config.apiUrl}/CatTipoCliente`;

  constructor(private http: HttpClient, private config: ConfigService) {
    super();
  }

  getByID(id: number): Observable<CatTipoCliente> {
    return this.http.get<CatTipoCliente>(`${this._url}/${id}`);
  }

  getAll(): Observable<CatTipoCliente[]> {
    return this.http.get<CatTipoCliente[]>(this._url);
  }

  saveNew(catTipoCliente: CatTipoCliente): Observable<CatTipoCliente> {
    // Implementa la lógica para guardar un nuevo CatTipoCliente
    // y devuelve un observable si es necesario.
    return this.http.post<CatTipoCliente>(this._url, catTipoCliente);
  }
}
