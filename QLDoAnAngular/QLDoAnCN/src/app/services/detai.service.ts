import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DeTai } from '../models/detai.model';
import { GiangVien } from '../models/giangvien.model'; // Đảm bảo đường dẫn đúng

@Injectable({
  providedIn: 'root'
})
export class DeTaiService {
  private apiUrl = 'https://localhost:7121/api/DeTais';
  private giangVienUrl = 'https://localhost:7121/api/GiangViens';
  private chuyenNganhUrl = 'https://localhost:7121/api/ChuyenNganhs'; // Đảm bảo URL đúng

  constructor(private http: HttpClient) { }

  getAllDeTai(): Observable<DeTai[]> {
    return this.http.get<DeTai[]>(this.apiUrl);
  }

  getDeTai(id: string): Observable<DeTai> {
    return this.http.get<DeTai>(`${this.apiUrl}/\${id}`);
  }

  addDeTai(deTai: DeTai): Observable<DeTai> {
    return this.http.post<DeTai>(this.apiUrl, deTai);
  }

  updateDeTai(id: string, deTai: DeTai): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, deTai);
  }
  
  deleteDeTai(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getAllGiangVien(): Observable<GiangVien[]> {
    return this.http.get<GiangVien[]>(this.giangVienUrl);
  }

  getAllChuyenNganh(): Observable<string[]> {
    return this.http.get<string[]>(this.chuyenNganhUrl);
  }
}