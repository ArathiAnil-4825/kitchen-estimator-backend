import { PrismaClient } from "@prisma/client";
import bcrypt from "bcrypt";
const prisma = new PrismaClient();
async function main() {
    const adminEmail = "admin@example.com";
    const contractorEmail = "contractor@example.com";
    const adminPass = await bcrypt.hash("admin123", 10);
    const contractorPass = await bcrypt.hash("contractor123", 10);
    await prisma.user.upsert({
        where: { email: adminEmail },
        create: { name: "Admin", email: adminEmail, passwordHash: adminPass, role: "ADMIN" },
        update: {}
    });
    await prisma.user.upsert({
        where: { email: contractorEmail },
        create: { name: "Contractor", email: contractorEmail, passwordHash: contractorPass, role: "CONTRACTOR" },
        update: {}
    });
}
main().finally(async () => {
    await prisma.$disconnect();
});
