import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigService } from '../config.service';
import { CatProducto } from '../../../domain/models/cat-producto/cat-producto';
import { CatProductoGateway } from '../../../domain/models/cat-producto/gateway/cat-producto-gateway';

@Injectable({
  providedIn: 'root',
})
export class CatProductoApiService extends CatProductoGateway {
  private _url = `${this.config.apiUrl}/CatProducto`;

  constructor(private http: HttpClient, private config: ConfigService) {
    super();
  }

  getByID(id: number): Observable<CatProducto> {
    return this.http.get<CatProducto>(`${this._url}/${id}`);
  }

  getAll(): Observable<CatProducto[]> {
    return this.http.get<CatProducto[]>(this._url);
  }

  saveNew(catProducto: CatProducto): Observable<CatProducto> {
    // Implementa la lógica para guardar un nuevo CatProducto
    // y devuelve un observable si es necesario.
    return this.http.post<CatProducto>(this._url, catProducto);
  }
}
