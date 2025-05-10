export interface DeTai {
    maDeTai: string;
    tenDeTai: string;
    moTa?: string | null;
    chuyenNganh?: string | null;
    thoiGianThucHien?: number | null;
    maGV?: string | null;
    trangThai?: string | null; 
    giangVien?: GiangVien; 
  }
  
import { GiangVien } from '../models/giangvien.model';