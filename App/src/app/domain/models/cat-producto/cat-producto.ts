export interface CatProducto {
  id: number;
  nombreProducto: string;
  imagenProducto: string;
  precio: number;
  ext: string;
  createdAt: Date;
  updatedAt: Date;
  deletedAt: Date | null;
}
