import { Component, OnInit } from '@angular/core';
import { DeTaiService } from '../services/detai.service';
import { DeTai } from '../models/detai.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PopupComponent } from '../shared/popup/popup.component';
import { HttpErrorResponse } from '@angular/common/http';
import { GiangVien } from '../models/giangvien.model'; // Đảm bảo đường dẫn đúng

@Component({
  selector: 'app-de-tai',
  standalone: true,
  imports: [CommonModule, FormsModule, PopupComponent],
  templateUrl: './detai.component.html',
  styleUrl: './detai.component.css'
})
export class DeTaiComponent implements OnInit {
  danhSachDeTai: DeTai[] = [];
  selectedDeTai: DeTai | null = null;
  isAddingNew: boolean = false;
  newDeTai: DeTai = { maDeTai: '', tenDeTai: '', moTa: '', chuyenNganh: '', thoiGianThucHien: null, maGV: '', trangThai: 'Chờ duyệt' };
  isEditing: boolean = false;
  editingDeTai: DeTai | null = null;
  errorMessage: string = '';
  successMessage: string = '';
  isViewingDetails: boolean = false;
  danhSachGiangVien: GiangVien[] = [];
  danhSachChuyenNganh: string[] = [];

  constructor(private deTaiService: DeTaiService) { }

  ngOnInit(): void {
    this.loadDanhSachDeTai();
    this.loadDanhSachGiangVien();
    this.loadDanhSachChuyenNganh();
  }

  loadDanhSachDeTai(): void {
    this.deTaiService.getAllDeTai().subscribe({
      next: (data) => {
        this.danhSachDeTai = data;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách đề tài.';
        console.error('Lỗi tải danh sách đề tài', error);
      }
    });
  }

  loadDanhSachGiangVien(): void {
    this.deTaiService.getAllGiangVien().subscribe({
      next: (data) => {
        this.danhSachGiangVien = data;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách giảng viên.';
        console.error('Lỗi tải danh sách giảng viên', error);
      }
    });
  }

  loadDanhSachChuyenNganh(): void {
    this.deTaiService.getAllChuyenNganh().subscribe({
      next: (data: any[]) => { // Giả định data là mảng các object
        this.danhSachChuyenNganh = data.map(item => item.tenChuyenNganh); // Ánh xạ để lấy tên chuyên ngành
        console.log('Danh sách chuyên ngành đã tải:', this.danhSachChuyenNganh);
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách chuyên ngành.';
        console.error('Lỗi tải danh sách chuyên ngành', error);
      }
    });
  }

  hienThiChiTiet(deTai: DeTai): void {
    this.selectedDeTai = deTai;
    this.isViewingDetails = true;
  }

  batDauThemMoi(): void {
    this.isAddingNew = true;
    this.newDeTai = { maDeTai: '', tenDeTai: '', moTa: '', chuyenNganh: '', thoiGianThucHien: null, maGV: '', trangThai: 'Chờ duyệt' };
  }

  batDauChinhSua(deTai: DeTai): void {
    this.editingDeTai = { ...deTai };
    this.isEditing = true;
  }

  dongPopupChiTiet(): void {
    this.isViewingDetails = false;
    this.selectedDeTai = null;
  }

  dongPopupThemMoi(): void {
    this.isAddingNew = false;
  }

  dongPopupChinhSua(): void {
    this.isEditing = false;
    this.editingDeTai = null;
  }

  themDeTai(): void {
    console.log('Dữ liệu gửi khi thêm:', this.newDeTai);
    this.deTaiService.addDeTai(this.newDeTai).subscribe({
      next: (response) => {
        this.successMessage = 'Thêm đề tài thành công.';
        this.loadDanhSachDeTai();
        this.isAddingNew = false;
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (error: HttpErrorResponse) => {
        this.errorMessage = 'Lỗi khi thêm đề tài.';
        console.error('Lỗi thêm đề tài', error);
        console.log('Response body (error):', error.error);
      }
    });
  }

  capNhatDeTai(): void {
    if (this.editingDeTai) {
      console.log('Dữ liệu gửi khi cập nhật:', this.editingDeTai);
      this.deTaiService.updateDeTai(this.editingDeTai.maDeTai, this.editingDeTai).subscribe({
        next: (response) => {
          this.successMessage = 'Cập nhật đề tài thành công.';
          this.loadDanhSachDeTai();
          this.isEditing = false;
          this.editingDeTai = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error: HttpErrorResponse) => {
          this.errorMessage = 'Lỗi khi cập nhật đề tài.';
          console.error('Lỗi cập nhật đề tài', error);
          console.log('Response body (error):', error.error);
        }
      });
    }
  }

  huyThemMoi(): void {
    this.isAddingNew = false;
  }

  huyChinhSua(): void {
    this.isEditing = false;
    this.editingDeTai = null;
  }

  xoaDeTai(maDeTai: string): void {
    if (confirm('Bạn có chắc chắn muốn xóa đề tài này?')) {
      console.log('Mã đề tài gửi khi xóa:', maDeTai);
      this.deTaiService.deleteDeTai(maDeTai).subscribe({ 
        next: (response) => {
          this.successMessage = 'Xóa đề tài thành công.';
          this.loadDanhSachDeTai();
          this.selectedDeTai = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error: HttpErrorResponse) => {
          this.errorMessage = 'Lỗi khi xóa đề tài.';
          console.error('Lỗi xóa đề tài', error);
          console.log('Response body (error):', error.error);
        }
      });
    }
  }
}