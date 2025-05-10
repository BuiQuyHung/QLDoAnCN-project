import { Component, OnInit } from '@angular/core';
import { GiangVienService } from '../services/giangvien.service';
import { GiangVien } from '../models/giangvien.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PopupComponent } from '../shared/popup/popup.component';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-giang-vien',
  standalone: true,
  imports: [CommonModule, FormsModule, PopupComponent],
  templateUrl: './giangvien.component.html',
  styleUrl: './giangvien.component.css'
})
export class GiangVienComponent implements OnInit {
  danhSachGiangVien: GiangVien[] = [];
  selectedGiangVien: GiangVien | null = null;
  isAddingNew: boolean = false;
  newGiangVien: GiangVien = { maGV: '', hoTen: '', email: '', soDienThoai: '', chuyenNganh: '' };
  isEditing: boolean = false;
  editingGiangVien: GiangVien | null = null;
  errorMessage: string = '';
  successMessage: string = '';
  isViewingDetails: boolean = false;
  danhSachChuyenNganh: string[] = []; // Mảng lưu trữ danh sách chuyên ngành

  constructor(private giangVienService: GiangVienService) { }

  ngOnInit(): void {
    this.loadDanhSachGiangVien();
    this.loadDanhSachChuyenNganh(); // Tải danh sách chuyên ngành khi component khởi tạo
  }

  loadDanhSachGiangVien(): void {
    this.giangVienService.getAllGiangVien().subscribe({
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
    console.log('Bắt đầu tải danh sách chuyên ngành...');
    this.giangVienService.getAllChuyenNganh().subscribe({
      next: (data) => {
        this.danhSachChuyenNganh = data;
        console.log('Danh sách chuyên ngành đã tải:', this.danhSachChuyenNganh);
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách chuyên ngành.';
        console.error('Lỗi tải danh sách chuyên ngành', error);
      }
    });
  }

  hienThiChiTiet(giangVien: GiangVien): void {
    this.selectedGiangVien = giangVien;
    this.isViewingDetails = true;
  }

  batDauThemMoi(): void {
    this.isAddingNew = true;
    this.newGiangVien = { maGV: '', hoTen: '', email: '', soDienThoai: '', chuyenNganh: '' };
  }

  batDauChinhSua(giangVien: GiangVien): void {
    this.editingGiangVien = { ...giangVien };
    this.isEditing = true;
  }

  dongPopupChiTiet(): void {
    this.isViewingDetails = false;
    this.selectedGiangVien = null;
  }

  dongPopupThemMoi(): void {
    this.isAddingNew = false;
  }

  dongPopupChinhSua(): void {
    this.isEditing = false;
    this.editingGiangVien = null;
  }

  themGiangVien(): void {
    console.log('Dữ liệu gửi khi thêm:', this.newGiangVien);
    this.giangVienService.addGiangVien(this.newGiangVien).subscribe({
      next: (response) => {
        this.successMessage = 'Thêm giảng viên thành công.';
        this.loadDanhSachGiangVien();
        this.isAddingNew = false;
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (error: HttpErrorResponse) => {
        this.errorMessage = 'Lỗi khi thêm giảng viên.';
        console.error('Lỗi thêm giảng viên', error);
        console.log('Response body (error):', error.error);
      }
    });
  }

  capNhatGiangVien(): void {
    if (this.editingGiangVien) {
      console.log('Dữ liệu gửi khi cập nhật:', this.editingGiangVien);
      this.giangVienService.updateGiangVien(this.editingGiangVien.maGV, this.editingGiangVien).subscribe({
        next: (response) => {
          this.successMessage = 'Cập nhật giảng viên thành công.';
          this.loadDanhSachGiangVien();
          this.isEditing = false;
          this.editingGiangVien = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error: HttpErrorResponse) => {
          this.errorMessage = 'Lỗi khi cập nhật giảng viên.';
          console.error('Lỗi cập nhật giảng viên', error);
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
    this.editingGiangVien = null;
  }

  xoaGiangVien(maGV: string): void {
    if (confirm('Bạn có chắc chắn muốn xóa giảng viên này?')) {
      console.log('Mã GV gửi khi xóa:', maGV);
      this.giangVienService.deleteGiangVien(maGV).subscribe({
        next: (response) => {
          this.successMessage = 'Xóa giảng viên thành công.';
          this.loadDanhSachGiangVien();
          this.selectedGiangVien = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error: HttpErrorResponse) => {
          this.errorMessage = 'Lỗi khi xóa giảng viên.';
          console.error('Lỗi xóa giảng viên', error);
          console.log('Response body (error):', error.error);
        }
      });
    }
  }
}