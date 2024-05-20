import { CatTipoCliente } from "../cat-tipo-cliente/cat-tipo-cliente";

export interface TblCliente {
  id: number;
  razonSocial: string;
  idTipoCliente: number;
  rfc: string;
  fechaDeCreacion: Date;
  createdAt: Date;
  updatedAt: Date;
  deletedAt: Date | null;
  tipoCliente: CatTipoCliente;
}
