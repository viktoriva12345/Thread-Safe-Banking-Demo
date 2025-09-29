# 🏦 Thread-Safe Banking Demo

This project demonstrates **multithreading and thread safety in C#** by simulating a simple banking system where multiple clients (threads) perform deposits and withdrawals on the same account.

---

## 🚀 Features
- ✅ Multiple threads (`Task.Run`) simulate concurrent clients  
- ✅ Thread safety using `lock` for balance updates  
- ✅ `ConcurrentQueue` for safe logging of transactions  
- ✅ Simulation of **race conditions prevention**  
- ✅ Console output showing concurrent operations and final account balance  

---
🧵 Concepts Demonstrated

Multithreading with Task.Run

Thread safety using lock and synchronization primitives

Concurrent collections (ConcurrentQueue)

Race condition prevention in shared resources

💡 Why this project?

This demo shows how to handle shared resources in a multi-threaded environment, which is a common requirement in real-world systems like:

Banking & financial applications

Reservation systems

Inventory management

Any concurrent data processing system
