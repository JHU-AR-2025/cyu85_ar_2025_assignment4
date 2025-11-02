# Assignment 4 – Augmented Reality

**Author:** Richal (Chenhao) Yu  
**Course:** EN.601.654/454 – Introduction to Augmented Reality (JHU)  
**Term:** Fall 2025  

---

## 📦 Repository Overview

This repository contains my solutions and materials for **Assignment 4** of the AR course.  
It includes:
.
├── q2/                      # Kalman filter source code
├── q3/                      # Unity Roll-a-Ball project and demo video
├── Assignment 4.pdf          # Assignment write-up
├── assignment 4 manuscript.pdf
├── assigment4_record.mov     # Demo video recording
└── README.md                 # This file

---

## 🧩 Q1. Public Repository & Transfer

This repository is **public** and has been **transferred to the JHU-AR-2025 GitHub Organization**.  
(https://github.com/JHU-AR-2025)

A `README.md` is provided for attribution and documentation, as required.

---

## 🧠 Q2. Kalman Filter for Pose Estimation

### 📄 Files:
- `q2/kf_with_measurement.py` – Kalman Filter **with** measurement updates  
- `q2/kf_without_measurement.py` – Kalman Filter **without** measurement updates  
- (Optional) `q2/simulate_data.py` – for generating sample trajectories and noisy observations

### ⚙️ Description:
This section implements a **constant-velocity Kalman Filter** to estimate the position and velocity of a simulated target.

#### **Part (e)** – *Kalman Filter with measurement*  
Performs both **predict** and **update** steps using simulated noisy position data.  
Results show convergence of estimated states to the ground truth.

#### **Part (f)** – *Kalman Filter without measurement*  
Runs only the **predict** step, demonstrating how uncertainty grows when no measurement correction is applied.

### ▶️ How to Run
```bash
cd q2
pip install numpy matplotlib
python kf_with_measurement.py
python kf_without_measurement.py
