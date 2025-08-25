import { Router } from "express";
import { prisma } from "../prisma.js";
import { authenticate, requireRole } from "../middleware/auth.js";

const router = Router();
router.use(authenticate, requireRole(["ADMIN"]));

// List pending bills
router.get("/bills/pending", async (_req, res) => {
  const bills = await prisma.bill.findMany({ where: { status: "PENDING" }, include: { project: true, contractor: true } });
  res.json(bills);
});

// Approve a bill
router.post("/bills/:id/approve", async (req, res) => {
  const billId = Number(req.params.id);
  const admin = (req as any).user as { userId: number };
  const bill = await prisma.bill.findUnique({ where: { id: billId } });
  if (!bill) return res.status(404).json({ error: "Bill not found" });
  if (bill.status !== "PENDING") return res.status(400).json({ error: "Bill not pending" });
  const updated = await prisma.bill.update({
    where: { id: billId },
    data: { status: "APPROVED", approvedAt: new Date(), approvedByAdminId: admin.userId }
  });
  await prisma.project.update({ where: { id: bill.projectId }, data: { status: "APPROVED" } });
  res.json(updated);
});

export default router;
