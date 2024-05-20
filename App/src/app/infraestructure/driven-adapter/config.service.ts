import { Injectable } from '@angular/core';
import { environment } from '../../environment';

@Injectable()
export class ConfigService {
  public get apiUrl(): string {
    return environment.apiUrl;
  }
}
