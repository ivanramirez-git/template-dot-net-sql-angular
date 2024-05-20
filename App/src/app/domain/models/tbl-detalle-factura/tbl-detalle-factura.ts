import { CatProducto } from '../cat-producto/cat-producto';
import { TblFactura } from '../tbl-factura/tbl-factura';

export interface TblDetalleFactura {
  id: number;
  idFactura: number;
  idProducto: number;
  cantidadDeProducto: number;
  precioUnitario: number;
  subTotal: number;
  notas: string;
  createdAt: Date;
  updatedAt: Date;
  deletedAt: Date | null;
  factura: TblFactura;
  producto: CatProducto;
}
